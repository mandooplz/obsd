// src/core/example.test.ts

import { describe, it, expect } from 'vitest';
import { add, subtract } from '../core/TicTacToe';


// MARK: Tests
describe('Core Example Functions', () => {
  it('add function should return the sum of two numbers', () => {
    expect(add(1, 2)).toBe(3);
    expect(add(-1, 1)).toBe(0);
    expect(add(0, 0)).toBe(0);
  });

  it('subtract function should return the difference of two numbers', () => {
    expect(subtract(5, 2)).toBe(3);
    expect(subtract(1, 3)).toBe(-2);
    expect(subtract(0, 0)).toBe(0);
  });
});
