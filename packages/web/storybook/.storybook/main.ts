import type { StorybookConfig } from '@storybook/react-vite';

export default {
  stories: [
    '../src/lib/**/*.stories.@(js|jsx|ts|tsx|mdx)',
    '../../**/*.stories.@(js|jsx|ts|tsx|mdx)',
  ],
  addons: [
    '@storybook/addon-essentials',
    '@storybook/addon-interactions',
    'storybook-addon-react-router-v6'
  ],
  framework: {
    name: '@storybook/react-vite',
    options: {
      builder: {
        viteConfigPath: 'packages/web/storybook/vite.config.ts',
      },
    },
  },
} satisfies StorybookConfig;


// To customize your Vite configuration you can use the viteFinal field.
// Check https://storybook.js.org/docs/react/builders/vite#configuration
// and https://nx.dev/recipes/storybook/custom-builder-configs
