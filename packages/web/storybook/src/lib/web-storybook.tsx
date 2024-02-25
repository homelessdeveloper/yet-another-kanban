import styles from './web-storybook.module.css';

/* eslint-disable-next-line */
export interface WebStorybookProps {}

export function WebStorybook(props: WebStorybookProps) {
  return (
    <div className={styles['container']}>
      <h1>Welcome to WebStorybook!</h1>
    </div>
  );
}

export default WebStorybook;
