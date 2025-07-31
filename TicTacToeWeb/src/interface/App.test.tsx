import { render, screen } from '@testing-library/react';
import { describe, it, expect } from 'vitest';
import App from './App';

describe('App', () => {
  it('renders the main heading', () => {
    render(<App />);
    const headingElement = screen.getByText(/샘플 React 프로젝트/i);
    expect(headingElement).toBeInTheDocument();
  });
});
