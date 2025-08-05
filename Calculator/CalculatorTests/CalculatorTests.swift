//
//  CalculatorTests.swift
//  CalculatorTests
//
//  Created by 김민우 on 8/5/25.
//
import Testing
@testable import Calculator


// MARK: Tests
@Suite("Calculator")
struct CalculatorTests {
    struct setUpCards {
        let calculatorRef: Calculator
        init() async throws {
            self.calculatorRef = await Calculator()
        }
    }
}
