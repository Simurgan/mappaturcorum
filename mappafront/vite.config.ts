import path from "path";
import react from "@vitejs/plugin-react";
import { defineConfig } from "vite";
import { viteRequire } from "vite-require";

export default defineConfig({
  plugins: [react(), viteRequire()],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
});
