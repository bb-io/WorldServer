# Blackbird.io WorldServer

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

WorldServer is an enterprise translation management system (TMS) developed by RWS. It is designed to streamline and manage the localization and translation processes of large-scale projects. WorldServer enables organizations to automate translation workflows, manage translation memory and terminology, and collaborate with multiple translation providers.

## Connecting

1. Navigate to Apps, and identify the **WorldServer** app. You can use search to find it.
2. Click _Add Connection_.
3. Name your connection for future reference e.g. 'My WorldServer connection'.
4. Fill in the necessary fields: 
	- Instance URL
	- Username
	- Password
5. Click **Connect**.
6. Make sure that connection was added successfully.

![connection](./image/README/connecting.png)

## Actions

### Tasks

- **Search tasks** Search tasks
- **Assign task** Assign task
- **Claim task** Claim task
- **Complete task step** Complete task step
- **Export task** Export task
- **Get task** Get task
- **Import task** Import task
- **Unclaim task** Unclaim task
- **Update task** Update task
- **Export all project tasks** This action allows to download all tasks from the chosen project in `.zip` file, where inputs like `Project ID` and `File type` are required.

### Projects

- **Complete project step** Complete project step
- **Get project** Get project
- **Search projects** Search projects
- **Update project** Update project

### Project groups

- **Create project group** Create project group
- **Get project group** Get project group
- **Search project groups** Search project groups

### Events
- **On project created**  Triggered when a new project was created
- **On project completed** Triggered when a project is completed
- **On task completed** Triggered when tasks are completed

### Files

- **Download file** Download task file
- **Upload file** Upload file

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
