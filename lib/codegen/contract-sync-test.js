const file = require('../../openapi.json');
const shapshot = require('./openapi.snapshot.json');

console.info('Looking for changes in openapi.json...');

if (JSON.stringify(file) !== JSON.stringify(shapshot)) {
  console.error('Contract has changed. Please run "npm run sync" to update the client.');
  process.exit(1);
}

console.info('Contracts are in sync.');
process.exit(0);
