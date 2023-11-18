# ZPI Project

## Kanban board

When creating a new item in the Kanban board, add a prefix to the item/issue name indicating the team name. This will make it easier to filter tasks.

**Prefixes for tasks:**
- `[ui]`
- `[front]` (frontend)
- `[back]` (backend)
- `[docs]` (documentation)
- `[pres]` (presentation/marketing)
- `[devops]` (DevOps)
- `[db]` (database)

Example: `[front] Create login page`

**Board automation**
- Creating a new issue will add a new task to the board (status `Todo`)
- Closing an issue will move the corresponding task on the board to `Done`
- Reopening an issue will move the task to `In Progress`
- Merging a pull request assigned to the task will move the task to `In Progress`
