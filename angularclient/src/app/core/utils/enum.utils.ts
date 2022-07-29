export function getEnumKeys(enumType: any) {
  return Object.keys(enumType).filter((value) => isNaN(Number(value)));
}

export function getEnumValues(enumType: any): number[] {
  return Object.keys(enumType)
    .filter((value) => !isNaN(Number(value)))
    .map((value) => parseInt(value));
}
