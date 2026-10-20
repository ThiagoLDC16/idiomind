import fs from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const projectRoot = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
const distDirectory = path.join(projectRoot, 'dist');
const ignoredAssetsDirectory = path.join(distDirectory, 'assets/node_modules');
const publishedAssetsDirectory = path.join(distDirectory, 'assets/vendor');
const ignoredPublicPath = '/assets/node_modules/';
const publishedPublicPath = '/assets/vendor/';
const textExtensions = new Set(['.css', '.html', '.js', '.json']);

if (!fs.existsSync(ignoredAssetsDirectory)) {
  throw new Error(`Expected Expo assets directory was not found: ${ignoredAssetsDirectory}`);
}

if (fs.existsSync(publishedAssetsDirectory)) {
  throw new Error(`Cloudflare assets directory already exists: ${publishedAssetsDirectory}`);
}

fs.cpSync(ignoredAssetsDirectory, publishedAssetsDirectory, { recursive: true });
fs.rmSync(ignoredAssetsDirectory, { recursive: true });

let updatedFileCount = 0;

for (const filePath of walkFiles(distDirectory)) {
  if (!textExtensions.has(path.extname(filePath))) {
    continue;
  }

  const contents = fs.readFileSync(filePath, 'utf8');

  if (!contents.includes(ignoredPublicPath)) {
    continue;
  }

  fs.writeFileSync(filePath, contents.replaceAll(ignoredPublicPath, publishedPublicPath));
  updatedFileCount += 1;
}

if (updatedFileCount === 0) {
  throw new Error(`No generated files referenced ${ignoredPublicPath}`);
}

console.log(
  `Prepared Cloudflare Pages assets: moved assets/node_modules to assets/vendor and updated ${updatedFileCount} generated file(s).`,
);

function* walkFiles(directory) {
  for (const entry of fs.readdirSync(directory, { withFileTypes: true })) {
    const entryPath = path.join(directory, entry.name);

    if (entry.isDirectory()) {
      yield* walkFiles(entryPath);
    } else if (entry.isFile()) {
      yield entryPath;
    }
  }
}
