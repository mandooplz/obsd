//
//  SorterTestts.swift
//  Sorter
//
//  Created by 김민우 on 7/30/25.
//
import Foundation
import Testing
@testable import Sorter


// MARK: Tests
@Suite("Sorter")
struct SorterTests {
    @Test func whenItemsIsEmpty() async throws {
        // given
        let sorterRef = Sorter([])
        
        // when
        await sorterRef.calculate()
        
        // then
        let issue = try #require(await sorterRef.issue)
        #expect(issue.reason == "itemsIsEmpty")
    }
    @Test func setIsUsedTrue() async throws {
        // given
        let sorterRef = Sorter([3.3, 2.2, 1.1])
        
        try await #require(sorterRef.isUsed == false)
                
        // when
        await sorterRef.calculate()
        
        // then
        await #expect(sorterRef.isUsed == true)
    }
    
    @Test func sort_whenReversed() async throws {
        // given
        let unsorted = [3.3, 2.2, 1.1]
        let sorted = [1.1, 2.2, 3.3]
        
        let sorterRef = Sorter(unsorted)
        
        // when
        await sorterRef.calculate()
        
        // then
        #expect(await sorterRef.issue == nil)
        #expect(await sorterRef.items == sorted)
    }
}
