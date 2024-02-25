import { Router } from "@yak/web/pages";
import { Providers } from "./providers";


/**
 * Wep application root.
 *
 * @since 0.0.1
 */
export function WebApp() {
  return (
    <Providers>
      <Router />
    </Providers >
  );
}

export default WebApp;
