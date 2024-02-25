import log from "loglevel";
import * as ReactDOM from 'react-dom/client';
import { WebApp } from "@yak/web/app";
import "./styles.css";


///  TODO: move this to configuration.
log.setLevel("TRACE");


const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(<WebApp />);
