const file = require('../../openapi.json');
const shapshot = require('./openapi.snapshot.json');

console.log('Looking for changes in openapi.json...');

if (JSON.stringify(file) !== JSON.stringify(shapshot)) {
  console.log('Contract has changed. Please run "npm run sync" to update the client.');
  process.exit(1);
}

console.log('Contracts are in sync.');
process.exit(0);
