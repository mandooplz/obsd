//
//  SortData.swift
//  Sorter
//
//  Created by 김민우 on 7/30/25.
//
import Foundation


// MARK: Object
actor Sorter: Sendable {
    // MARK: core
    init(_ items: [Double]) {
        self.items = items
        self.isUsed = false
    }
    
    // MARK: state
    public internal(set) var items: [Double]
    public internal(set) var isUsed: Bool
    
    public internal(set) var issue: ObjectIssue?
    public func setIssue(_ error: Error) {
        self.issue = .init(error.rawValue)
    }
    public func resetIssue() {
        self.issue = nil
    }
    
    
    // MARK: action
    public func calculate() {
        print("setUp start")
        
        // capture
        guard items.count > 0 else {
            setIssue(.itemsIsEmpty)
            return
        }
        guard isUsed == false else {
            setIssue(.sorterIsUsed)
            return
        }
        
        // mutate
        self.items.sort()
        self.isUsed = true
    }
    
    
    // MARK: value
    public struct ObjectIssue: Sendable, Hashable {
        public let reason: String
        
        public init(_ reason: String) {
            self.reason = reason
        }
    }
    public enum Error: String, Swift.Error {
        case itemsIsEmpty
        case sorterIsUsed
    }
}

