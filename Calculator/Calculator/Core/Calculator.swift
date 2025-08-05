//
//  Calculator.swift
//  Calculator
//
//  Created by 김민우 on 8/5/25.
//
import Foundation


// MARK: Object
@MainActor @Observable
final class Calculator: Sendable {
    // MARK: core
    init() {
        CalculatorManager.register(self)
    }
    func delete() {
        CalculatorManager.unregister(self.id)
    }
    
    
    // MARK: state
    nonisolated let id = ID()
    
    var nums: [NumCard.Target: NumCard.ID] = [:]
    
    var result: Int?
    
    
    // MARK: action
    func setUpCards() {
        // capture
        guard nums.isEmpty else {
            print("이미 setUp된 상태입니다.")
            return
        }
        
        // mutate
        for numTarget in NumCard.Target.allCases {
            let numCardRef = NumCard(calculator: self.id,
                                     numberTarget: numTarget)
            self.nums[numTarget] = numCardRef.id
        }
    }
    func clear() { }
    func getResult() { }
    
    
    // MARK: value
    @MainActor
    struct ID: Sendable, Hashable {
        let id = UUID()
        nonisolated init() { }
    }
}


// MARK: ObjectManager
@MainActor @Observable
fileprivate final class CalculatorManager: Sendable {
    // MARK: state
    static var container: [Calculator.ID: Calculator] = [:]
    static func register(_ object: Calculator) {
        container[object.id] = object
    }
    static func unregister(_ id: Calculator.ID) {
        container[id] = nil
    }
}
