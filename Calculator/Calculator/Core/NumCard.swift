//
//  NumCard.swift
//  Calculator
//
//  Created by 김민우 on 8/5/25.
//
import Foundation


// MARK: Object
@MainActor @Observable
final class NumCard: Sendable {
    // MARK: core
    init(calculator: Calculator.ID,
         numberTarget: Target) {
        self.calculator = calculator
        self.target = numberTarget
        
        NumCardManager.register(self)
    }
    func delete() {
        NumCardManager.unregister(self.id)
    }
    
    
    // MARK: state
    nonisolated let id = ID()
    nonisolated let calculator: Calculator.ID
    nonisolated let target: Target
    
    
    // MARK: action
    
    
    // MARK: value
    @MainActor
    struct ID: Sendable, Hashable {
        let id = UUID()
        nonisolated init() { }
        
        var isExist: Bool {
            NumCardManager.container[self] != nil
        }
        var ref: NumCard? {
            NumCardManager.container[self]
        }
    }
    enum Target: Int, Sendable, Hashable, CaseIterable {
        case zero, one, two, three, four, five, six, seven, eight, nine
    }
}


// MARK: ObjectManager
@MainActor @Observable
fileprivate final class NumCardManager: Sendable {
    // MARK: state
    static var container: [NumCard.ID: NumCard] = [:]
    static func register(_ object: NumCard) {
        container[object.id] = object
    }
    static func unregister(_ id: NumCard.ID) {
        container[id] = nil
    }
}

