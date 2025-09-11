
export function singularize(word: string): string {
  if (word.endsWith("ies") && word.length > 3) {
    const beforeY = word[word.length - 4];
    if (!"aeiou".includes(beforeY)) {
      return word.slice(0, -3) + "y";
    }
  }
  if (word.endsWith("es") && word.length > 2) {
    return word.slice(0, -2);
  }
  if (word.endsWith("s") && word.length > 1) {
    return word.slice(0, -1);
  }
  return word;
}