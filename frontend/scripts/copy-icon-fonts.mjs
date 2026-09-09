import crypto from 'node:crypto';
import fs from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const projectRoot = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
const packageFontsDirectory = path.join(
  projectRoot,
  'node_modules/@expo/vector-icons/build/vendor/react-native-vector-icons/Fonts',
);
const distFontsDirectory = path.join(
  projectRoot,
  'dist/assets/node_modules/@expo/vector-icons/build/vendor/react-native-vector-icons/Fonts',
);
const fonts = ['MaterialIcons.ttf', 'Ionicons.ttf'];

fs.mkdirSync(distFontsDirectory, { recursive: true });

for (const font of fonts) {
  const sourcePath = path.join(packageFontsDirectory, font);
  const contents = fs.readFileSync(sourcePath);
  const hash = crypto.createHash('md5').update(contents).digest('hex');
  const destinationName = `${path.basename(font, '.ttf')}.${hash}.ttf`;

  fs.writeFileSync(path.join(distFontsDirectory, destinationName), contents);
}
