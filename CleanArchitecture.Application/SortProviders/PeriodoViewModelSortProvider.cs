using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Periodos;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.SortProviders;

public sealed class PeriodoViewModelSortProvider : ISortingExpressionProvider<PeriodoViewModel, Periodo>
{
    private static readonly Dictionary<string, Expression<Func<Periodo, object>>> s_expressions = new()
    {
        { "fechaInicio", periodo => periodo.FechaInicio },
        { "fechaFinal", periodo => periodo.FechaFinal },
        { "nombre", periodo => periodo.Nombre }
    };

    public Dictionary<string, Expression<Func<Periodo, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}