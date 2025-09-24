export async function fileToBase64Bytes(file: File | Blob): Promise<string> {
  const ab = await file.arrayBuffer();
  const binary = String.fromCharCode(...new Uint8Array(ab));
  const base64 = typeof btoa !== "undefined"
    ? btoa(binary)
    : Buffer.from(binary, "binary").toString("base64");
  return base64;
}

export function centsToDecimal(cents: number): number {
  return Math.round(Number(cents || 0)) / 100;
}

export function decimalToCents(value: number): number {
  return Math.round(Number(value || 0) * 100);
}
