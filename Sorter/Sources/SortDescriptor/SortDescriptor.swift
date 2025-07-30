//
//  SortDescriptor.swift
//  Sorter
//
//  Created by 김민우 on 7/30/25.
//
import Foundation


// MARK: Object
@globalActor
actor SortDescriptor {
    // MARK: core
    public static let shared = SortDescriptor()
    
    
    // MARK: state
    public nonisolated let id = ID()
    
    
    // MARK: action
    
    
    // MARK: value
    @SortDescriptor
    public struct ID: Sendable, Hashable {
        public let value = UUID()
        public nonisolated init() {}
        
        public var isExist: Bool {
            SortDescriptorManager.container[self] != nil
        }
        public var ref: SortDescriptor? {
            SortDescriptorManager.container[self]
        }
    }
    
}


// MARK: ObjectManager
@SortDescriptor
fileprivate final class SortDescriptorManager: Sendable {
    // MARK: state
    static var container: [SortDescriptor.ID: SortDescriptor] = [:]
    static func register(_ object: SortDescriptor) {
        container[object.id] = object
    }
    static func unregister(_ id: SortDescriptor.ID) {
        container[id] = nil
    }
}
