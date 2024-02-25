import { FC, PropsWithChildren, StrictMode, useState } from 'react';
import { ReactQueryDevtools } from "@tanstack/react-query-devtools"
import { Toaster } from "react-hot-toast";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter } from 'react-router-dom';
import { GlobalModalManager } from '@yak/web/shared/modal-manager';


/**
 * The Providers component provides various context providers and wrappers for your application.
 *
 * @since 0.0.1
 */
export const Providers: FC<PropsWithChildren> = ({ children }) => {
  const [client] = useState(new QueryClient())
  return (
    <StrictMode>
      <BrowserRouter>
        <QueryClientProvider client={client}>
          {/* -- ReactQuery stuff */}
          <ReactQueryDevtools initialIsOpen={false} position="bottom" />

          {/* -- Notifications */}
          <Toaster position={"bottom-right"} />

          {/* -- MODALS */}
          <GlobalModalManager.Provider />

          {/* -- CONTENT */}
          {children}
        </QueryClientProvider>
      </BrowserRouter>
    </StrictMode>
  )
}

