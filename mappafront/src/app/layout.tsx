import type { Metadata } from "next";
import "@/assets/globals.scss";

export const metadata: Metadata = {
  title: "Mappa Turcorum",
  description: "Mappa project",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>{children}</body>
    </html>
  );
}
