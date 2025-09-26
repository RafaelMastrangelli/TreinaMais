export async function fileToBase64Bytes(file: File | Blob): Promise<string> {
  const ab = await file.arrayBuffer();
  const uint8Array = new Uint8Array(ab);
  
  // Convert Uint8Array to binary string in chunks to avoid stack overflow
  let binary = '';
  const chunkSize = 8192; // Process in 8KB chunks
  
  for (let i = 0; i < uint8Array.length; i += chunkSize) {
    const chunk = uint8Array.slice(i, i + chunkSize);
    binary += String.fromCharCode(...chunk);
  }
  
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
