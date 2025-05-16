# Backstory
You are a developer at EnergyCom, an electricity company that manages thousands of electricity meters across multiple countries. You’ve taken over a codebase left behind by a developer who didn’t finish the project before leaving, and we’d like you to finish what they started. The application ingests data from several different sources, and when run right now simply makes that data available, but doesn’t do anything with it. 

## Your task
Your job is to expand the application such that the data can be organized and presented in a way where it is possible to reliably answer:

- How much electricity is produced by our devices?
- What kinds of devices do we run, and where?
- Any other information you find pertinent to the data you have available.

As this needs to be done urgently, you will only have at most 4 hours to build something that is useful. Your priority should be functionality and impact, so while information clarity is important, visual aesthetics and shine are not. EnergyCom is aware that this isn’t much time, so you’ll be expected to prioritize your time on the things you find important, to reason about why you’ve made the choices you have, and to have a plan for further work than what you can do in those 4 hours. 

The previous developer started a Energycom.Analysis project that has methods to retrieve data from the API and database directly. You are free to continue with this program or you may create your own (in whatever programming language you prefer). This project will still be useful to helop you understansd what kind of data you are dealing with. Additionally there is API documention at a /scalar endpoint on the ingestion service. You can find this endpoint in the aspire dashboard, or use the helper method to reach this.

![alt text](docs/scalar_aspire.png)

### Existing Architecture



### Requirements
- You should spend no more than 4 hours on this programming assignment.
- You may not use LLM's to solve any part of this task. We are not against using AI in coding assistance, but this task is to show your thought process and skill level, not your ability to prompt engineer.
- If you make changes to what is required to run the application in any way, you must ensure this README.md is updated under the Setup section. If we cannot run your solution it is likely an automatic failure.
- You are required to use git as you work on the project.
- We will be checking the git commit history in terms of both content and the commit message.
- You are encouraged to document your reasoning, priorities and general thoughts as you go along, in the THOUGHTS.MD markdown file.
- You may not change the applications that create the data in any way, at all - consider them ‘black boxes’ that simply create data. What you do with that data from that point on is 100% up to you, whether you want to stuff it in memory, in a database, in a file or whatever you’d like.

### Pre-requisites

install [dotnet 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) 
install [docker](https://www.docker.com/) 

### Helpful starting tips

The application runs in dotnet [aspire](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview). This is analogous to docker compose with some extra bells and whistles. You can run the entire solution via the command line from the /src folder

From source:
```bash
    dotnet run --project Energycom.AppHost/Energycom.AppHost.csproj
```

or by running the apphost project with your IDE of choice (VSCode, Rider, VisualStudio etc)



