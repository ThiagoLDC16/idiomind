export type DiffSegment = { type: 'keep' | 'remove' | 'add'; text: string };

function normalize(word: string): string {
  return word.toLowerCase().replace(/[^\p{L}\p{N}]/gu, '');
}

export function diffWords(original: string, corrected: string): DiffSegment[] {
  const a = original.trim().split(/\s+/);
  const b = corrected.trim().split(/\s+/);
  const na = a.map(normalize);
  const nb = b.map(normalize);
  const m = a.length, n = b.length;
  const dp = Array.from({ length: m + 1 }, () => new Array(n + 1).fill(0));
  for (let i = 1; i <= m; i++)
    for (let j = 1; j <= n; j++)
      dp[i][j] = na[i - 1] === nb[j - 1] ? dp[i - 1][j - 1] + 1 : Math.max(dp[i - 1][j], dp[i][j - 1]);

  const result: DiffSegment[] = [];
  let i = m, j = n;
  while (i > 0 || j > 0) {
    if (i > 0 && j > 0 && na[i - 1] === nb[j - 1]) {
      result.unshift({ type: 'keep', text: a[i - 1] });
      i--; j--;
    } else if (j > 0 && (i === 0 || dp[i][j - 1] >= dp[i - 1][j])) {
      result.unshift({ type: 'add', text: b[j - 1] });
      j--;
    } else {
      result.unshift({ type: 'remove', text: a[i - 1] });
      i--;
    }
  }
  return result;
}
