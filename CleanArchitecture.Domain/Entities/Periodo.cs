using System;

namespace CleanArchitecture.Domain.Entities;

public class Periodo : Entity
{
    public DateOnly FechaInicio { get; private set; }
    public DateOnly FechaFinal { get; private set; }
    public string Nombre { get; private set; }

    public Periodo(
        Guid id,
        DateOnly fechaInicio,
        DateOnly fechaFinal,
        string nombre) : base(id)
    {
        FechaInicio = fechaInicio;
        FechaFinal = fechaFinal;
        Nombre = nombre;
    }

    public void SetFechaInicio(DateOnly fechaInicio)
    {
        FechaInicio = fechaInicio;
    }

    public void SetFechaFinal(DateOnly fechaFinal)
    {
        FechaFinal = fechaFinal;
    }

    public void SetNombre(string nombre)
    {
        Nombre = nombre;
    }
}