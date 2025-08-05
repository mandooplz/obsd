//
//  OperatorCard.swift
//  Calculator
//
//  Created by 김민우 on 8/5/25.
//
import Foundation


// MARK: Object
@MainActor @Observable
final class OperatorCard: Sendable {
    // MARK: core
    init(calculator: Calculator.ID) {
        self.calculator = calculator
        
        OperatorCardManager.register(self)
    }
    func delete() {
        OperatorCardManager.unregister(self.id)
    }
    
    
    // MARK: state
    nonisolated let id = ID()
    nonisolated let calculator: Calculator.ID
    
    
    // MARK: action
    
    
    // MARK: value
    @MainActor
    struct ID: Sendable, Hashable {
        let id = UUID()
        nonisolated init() { }
        
        var isExist: Bool {
            OperatorCardManager.container[self] != nil
        }
        var ref: OperatorCard? {
            OperatorCardManager.container[self]
        }
    }
    enum Target: Sendable, Hashable, CaseIterable {
        case add, subtract, multiply, divide
    }
}


// MARK: ObjectManager
@MainActor @Observable
fileprivate final class OperatorCardManager: Sendable {
    // MARK: state
    static var container: [OperatorCard.ID: OperatorCard] = [:]
    static func register(_ object: OperatorCard) {
        container[object.id] = object
    }
    static func unregister(_ id: OperatorCard.ID) {
        container[id] = nil
    }
}


