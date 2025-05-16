# Backstory
You are a developer at EnergyCom, an electricity company that manages thousands of electricity meters across multiple countries. You’ve taken over a codebase left behind by a developer who didn’t finish the project before leaving, and we’d like you to finish what they started. The aim of the project is to produce an Analysis application that can answer high-level questions
based on reading raw data.

## Your task
We would like you to implement whatever is necessary for the EnergyCom.Analysis project to provide answers to these questions:

- How much electricity is produced by our devices?
- What devices do we have, and what information do we have on them?
- Any other information you find pertinent to make available, based on the raw data and what you can imagine it could be used for.

At bare minimum there must be callable methods that provide answers to the questions above, and it must be possible to run the application 
and get at that information - whether through the commandline, a dashboard, raw files or whatever you find appropriate.

The previous developer started Energycom.Analysis, which has methods to retrieve data from the API and database directly. You are free to continue with this program 
or you may create your own (in whatever programming language you prefer). This project will still be useful to helop you understand what kind of data you are dealing with. 
Additionally there is API documention at a /scalar endpoint on the ingestion service. You can find this endpoint in the aspire dashboard, or use the helper method to reach this.

![alt text](docs/scalar_aspire.png)

### Existing Architecture

### Take-home requirements
- You should spend up to, but no more than 4 hours writing code on this take-home assignment. Please write down how long you spent at the end.
- You should prioritize your time on the things you find important, to reason about why you’ve made the choices you have, and to have a plan for further work
  than what you can do in those 4 hours. In other words, build the application in a way where someone else could take it over and continue work on it.
- Please do not use LLM's to solve any part of this task.
- If you make changes to what is required to run the application in any way, you must ensure this README.md is updated under the Setup section. If we cannot run your solution it is likely an automatic failure.
- You are required to use git and git commits as you work on the project. We'll be checking the commit history and contents as part of our assessment.
- You are encouraged to document your reasoning, priorities and general thoughts as you go along, in the THOUGHTS.MD markdown file.
- You may not change the code or contents of the project called `EnergyCom.Ingestion` - consider it a black box that simply create data.
- You can create whatever extra projects you'd like, and tweak the others, as long as you remember to leave instructions on how to run them and set them up in this file.

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



