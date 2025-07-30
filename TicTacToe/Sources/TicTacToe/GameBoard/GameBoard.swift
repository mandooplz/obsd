//
//  GameBoard.swift
//  TicTacToe
//
//  Created by 김민우 on 7/30/25.
//
import Foundation


// MARK: Object
@MainActor
public final class GameBoard {
    // MARK: core
    init() {
        GameBoardManager.register(self)
    }
    func delete() {
        GameBoardManager.unregister(self.id)
    }
    
    // MARK: state
    nonisolated let id = ID()
    public nonisolated let createdAt: Date = .now

    public internal(set) var currentPlayer: Player = .X
    internal func changePlayer() {
        switch currentPlayer {
        case .X:
            self.currentPlayer = .O
        case .O:
            self.currentPlayer = .X
        }
    }
    
    public internal(set) var cards: [Int: GameCard.ID] = [:]
    internal var XPositions: Set<Int> {
        let positions = self.cards
            .filter { $1.ref?.owner == .X }
            .map { $0.key }
        
        return Set(positions)
    }
    internal var OPositions: Set<Int> {
        let positions = self.cards
            .filter { $1.ref?.owner == .O }
            .map { $0.key }
        
        return Set(positions)
    }
    
    public var isEnd: Bool = false
    public var result: GameResult?
    
    public var issue: ObjectIssue?
    func setIssue(_ error: Error) {
        self.issue = .init(error.rawValue)
    }
    
    package var callback: Callback?
    package func setCallback(_ callback: @escaping Callback) {
        self.callback = callback
    }
    
    
    
    // MARK: action
    public func setUp() async {
        // capture
        await callback?()
        guard id.isExist else {
            setIssue(.gameBoardIsDeleted)
            print(#file, #line, #function, "GameBoard가 존재하지 않아 실행 취소됩니다.")
            return
        }
        guard cards.isEmpty else {
            setIssue(.alreadySetUp)
            print(#file, #line, #function, "이미 setUp된 상태입니다.")
            return
        }
        
        // mutate
        for position in 0..<9 {
            let gameCardRef = GameCard(position: position,
                                       board: self.id)
            self.cards[position] = gameCardRef.id
        }
    }

    
    // MARK: value
    @MainActor
    public struct ID: Sendable, Hashable, Identifiable {
        public let id = UUID()
        nonisolated init() { }
        
        public var isExist: Bool {
            GameBoardManager.container[self] != nil
        }
        public var ref: GameBoard? {
            GameBoardManager.container[self]
        }
    }
    
    public enum Player: Sendable, Hashable {
        case X, O
    }
    
    public enum GameResult: Sendable, Hashable {
        case draw
        case win(Player)
    }
    
    public struct ObjectIssue: Sendable, Hashable, Identifiable {
        public let id = UUID()
        public let reason: String
        
        public init(_ reason: String) {
            self.reason = reason
        }
    }
    
    package typealias Callback = @Sendable () async -> Void
    
    public enum Error: String, Swift.Error {
        case gameBoardIsDeleted
        case alreadySetUp
    }
    
    
}



// MARK: ObjectManager
@MainActor
fileprivate final class GameBoardManager {
    // MARK: state
    static var container: [GameBoard.ID: GameBoard] = [:]
    static func register(_ object: GameBoard) {
        container[object.id] = object
    }
    static func unregister(_ id: GameBoard.ID) {
        container[id] = nil
    }
}
