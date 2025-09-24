/**
 * Formats a number value as Brazilian currency (R$)
 * Uses Brazilian locale with comma for decimals and proper currency symbol
 * @param value The numeric value to format
 * @returns Formatted currency string (e.g., "R$ 299,90")
 */
export function formatCurrency(value: number): string {
  if (value == null || isNaN(value)) {
    return 'R$ 0,00';
  }

  return value.toLocaleString("pt-BR", {
    style: "currency",
    currency: "BRL"
  });
}

/**
 * Converts cents to formatted Brazilian currency display
 * @param cents The value in cents (e.g., 29990 for R$ 299,90)
 * @returns Formatted currency string
 */
export function centsToDisplay(cents: number): string {
  const value = (cents || 0) / 100;
  return formatCurrency(value);
}