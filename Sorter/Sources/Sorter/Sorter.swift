//
//  Sorter.swift
//  Sorter
//
//  Created by 김민우 on 7/30/25.
//
import Foundation
import Collections


// MARK: Object
@globalActor
public actor Sorter: Sendable {
    // MARK: core
    public static let shared = Sorter()
    
    
    // MARK: state
    public nonisolated let id = ID()
    
    
    // MARK: action
    
    
    // MARK: value
    @Sorter
    public struct ID: Sendable, Hashable {
        public let value = UUID()
        public nonisolated init() {}
        
        public var isExist: Bool {
            SorterManager.container[self] != nil
        }
        public var ref: Sorter? {
            SorterManager.container[self]
        }
    }
    public struct Ticket<T: Sendable>: Sendable, Hashable {
        public let id: UUID = .init()
        public let createdAt: Date = .now
        public var sortData: [T]
        
        public init(sortData: [T]) {
            self.sortData = sortData
        }
        
        public func hash(into hasher: inout Hasher) {
            hasher.combine(id)
        }
        public static func == (lhs: Sorter.Ticket<T>,
                               rhs: Sorter.Ticket<T>) -> Bool {
            lhs.id == rhs.id
        }
    }
}


// MARK: ObjectManager
@Sorter
fileprivate final class SorterManager: Sendable {
    // MARK: state
    static var container: [Sorter.ID: Sorter] = [:]
    static func register(_ object: Sorter) {
        container[object.id] = object
    }
    static func unregister(_ id: Sorter.ID) {
        container[id] = nil
    }
}

