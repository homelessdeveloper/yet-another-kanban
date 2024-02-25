import { render } from '@testing-library/react';

import WebStorybook from './web-storybook';

describe('WebStorybook', () => {
  it('should render successfully', () => {
    const { baseElement } = render(<WebStorybook />);
    expect(baseElement).toBeTruthy();
  });
});
