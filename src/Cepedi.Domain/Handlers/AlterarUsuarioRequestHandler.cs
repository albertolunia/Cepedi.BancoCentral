﻿using Cepedi.BancoCentral.Domain.Entities;
using Cepedi.BancoCentral.Domain.Repository;
using Cepedi.Shareable.Requests;
using Cepedi.Shareable.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cepedi.BancoCentral.Domain.Handlers;
public class AlterarUsuarioRequestHandler : IRequestHandler<AlterarUsuarioRequest, AlterarUsuarioResponse>
{
    private readonly ILogger<AlterarUsuarioRequestHandler> _logger;
    private readonly IUsuarioRepository _usuarioRepository;

    public AlterarUsuarioRequestHandler(IUsuarioRepository usuarioRepository, ILogger<AlterarUsuarioRequestHandler> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    public async Task<AlterarUsuarioResponse> Handle(AlterarUsuarioRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorIdAsync(request.IdUsuario);

            usuario.Nome = request.Nome;
            usuario.DataNascimento = request.DataNascimento;
            usuario.Celular = request.Celular;
            usuario.CelularValidado = request.CelularValidado;
            usuario.Email = request.Email;
            usuario.Cpf = request.Cpf;

            await _usuarioRepository.AlterarUsuarioAsync(usuario);

            return new AlterarUsuarioResponse(usuario.Id, usuario.Nome);

        }
        catch
        {
            _logger.LogError("Ocorreu um erro durante a execução");
            throw;
        }
    }
}
