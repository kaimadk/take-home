using Energycom.AppHost;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresServerResource> postgres = builder.AddPostgres("PostgresServer")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithEndpoint(port: 25432, targetPort: 5432, scheme: "https", name: "postgres-port" )
    .WithPgAdmin(opts => opts.WithHostPort(23371));


IResourceBuilder<PostgresDatabaseResource> db = postgres.AddDatabase("EnergycomDb").WithCreateCommand(true);

var backend = builder.AddProject<Energycom_Ingestion>("Ingestion")
    .WithReference(db)
    .WaitFor(db)
    .WithDashboardResource("scalar", "scalar", "scalar");

var analysis = builder.AddProject<Energycom_Analysis>("Analysis")
    .WithReference(db)
    .WithReference(backend)
    .WaitFor(db)
    .WaitFor(backend);



builder.Build().Run();