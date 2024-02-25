
import { defineConfig } from '@kubb/core'
import createSwagger from '@kubb/swagger'
import createSwaggerTanstackQuery from '@kubb/swagger-tanstack-query'
import createSwaggerTS from '@kubb/swagger-ts'
import createSwaggerClient from '@kubb/swagger-client'
import createSwaggerZod from "@kubb/swagger-zod"

export default defineConfig({
  input: {
    path: './apps/api/docs/openapi/openapi.json',
  },
  output: {
    path: './packages/web/shared/api/src/lib/generated',
  },
  plugins: [
    createSwagger({
      output: false,
      validate: false
    }),
    createSwaggerZod({
      output: {
        path: './zod',
      }
    }),
    createSwaggerClient({
      output: {
        path: './api/axios',
      },
      client: {
        importPath: '../../../axios-client',
      },
    }),
    createSwaggerTS({}),
    createSwaggerTanstackQuery({
      parser: 'zod',
      output: {
        path: './hooks',
      },
      client: {
        importPath: '../../axios-client',
      },
    }),
  ],
})
