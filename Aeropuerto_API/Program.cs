using AeropuertoAPI.Models;
using AeropuertoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("OracleDb");

builder.Services.AddSingleton(new DatabaseSettings
{
    ConnectionString = connectionString ?? throw new InvalidOperationException("No se encontró la cadena de conexión OracleDb.")
});
//servicios 
builder.Services.AddScoped<TestConnectionService>();
builder.Services.AddScoped<AerolineaService>();
builder.Services.AddScoped<ModeloAvionService>();
builder.Services.AddScoped<AvionService>();
builder.Services.AddScoped<AeropuertoService>();
builder.Services.AddScoped<ProgramaVueloService>();
builder.Services.AddScoped<VueloService>();
builder.Services.AddScoped<PasajeroService>();
builder.Services.AddScoped<ReservaService>();
builder.Services.AddScoped<VentaBoletoService>();
builder.Services.AddScoped<TransaccionPagoService>();
builder.Services.AddScoped<MetodoPagoService>();
builder.Services.AddScoped<PuntoVentaService>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<PuestoService>();
builder.Services.AddScoped<DepartamentoService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<RepuestoService>();
builder.Services.AddScoped<CategoriaRepuestoService>();
builder.Services.AddScoped<HangarService>();
builder.Services.AddScoped<MantenimientoAvionService>();
builder.Services.AddScoped<MovimientoRepuestoService>();
builder.Services.AddScoped<RepuestoUtilizadoService>();
builder.Services.AddScoped<AsignacionHangarService>();
builder.Services.AddScoped<TripulacionService>();
builder.Services.AddScoped<DetalleVentaBoletoService>();
builder.Services.AddScoped<DiasVueloService>();
builder.Services.AddScoped<EscalaTecnicaService>();
builder.Services.AddScoped<OrdenCompraRepuestoService>();
builder.Services.AddScoped<AsistenciaService>();
builder.Services.AddScoped<LicenciaEmpleadoService>();
builder.Services.AddScoped<PlanillaService>();
builder.Services.AddScoped<SesionUsuarioService>();
builder.Services.AddScoped<BusquedaVueloService>();
builder.Services.AddScoped<ClickDestinoService>();
builder.Services.AddScoped<CarritoCompraService>();
builder.Services.AddScoped<ItemCarritoService>();
builder.Services.AddScoped<PreferenciaClienteService>();
builder.Services.AddScoped<PromocionService>();
builder.Services.AddScoped<UsoPromocionService>();
builder.Services.AddScoped<AuditoriaService>();
builder.Services.AddScoped<ArrestoService>();
builder.Services.AddScoped<ObjetoPerdidoService>();
builder.Services.AddScoped<DetalleOrdenCompraService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
