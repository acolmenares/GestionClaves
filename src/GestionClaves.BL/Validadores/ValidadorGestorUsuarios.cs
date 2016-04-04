using GestionClaves.Modelos;
using ServiceStack.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.FluentValidation.Results;
using GestionClaves.Modelos.Interfaces;
using GestionClaves.Modelos.Entidades;
using GestionClaves.Modelos.Servicio;

namespace GestionClaves.BL.Validadores
{
    public class ValidadorGestorUsuarios:IValidadorGestorUsuarios
    {

        public IValidador<ActualizarClave> ValidadorActualizarClave { get; set; }
        public IValidador<Usuario> ValidadorLoginContrasena { get; set; }
        public IValidador<Usuario> ValidadorLogin { get; set; }
        public IValidador<GenerarContrasena> ValidadorGenerarContrasena { get; set; }

        public ValidadorGestorUsuarios()
        {
            ValidadorActualizarClave = new ValidadorActualizarClave();
            ValidadorLoginContrasena = new ValidadorLoginContrasena();
            ValidadorLogin = new ValidadorLogin();
            ValidadorGenerarContrasena = new ValidadorGenerarContrasena();

        }

        public void ValidarPeticion(ActualizarClave request)
        {
            ValidadorActualizarClave.ValidateAndThrow(request);
        }

        public void ValidarLoginContrasena(Usuario usuario)
        {
            ValidadorLoginContrasena.ValidateAndThrow(usuario);
        }

        public void ValidarPeticion(GenerarContrasena request)
        {
            ValidadorGenerarContrasena.ValidateAndThrow(request);
        }

        public void ValidarLogin(Usuario usuario)
        {
            ValidadorLogin.ValidateAndThrow(usuario);
        }
    }

    


    public class ValidadorLoginContrasena:ValidadorBase<Usuario>,IValidador<Usuario>
    {
        public ValidadorLoginContrasena()
        {
            MensajeCuandoInstanciaEsNull = "Usuario / Contraseña inválidos";
            RuleFor(f => f.Activo).Cascade(CascadeMode.StopOnFirstFailure).NotNull().Must(v => v.Value==true).WithErrorCode("").WithMessage("Usuario Inactivo");  
        }                   
    }

    public class ValidadorLogin : ValidadorBase<Usuario>, IValidador<Usuario>
    {
        public ValidadorLogin()
        {
            MensajeCuandoInstanciaEsNull = "Usuario No Existe";
            RuleFor(f => f.Activo).Cascade(CascadeMode.StopOnFirstFailure).NotNull().Must(v => v.Value == true).WithErrorCode("").WithMessage("Usuario Inactivo");
            RuleFor(f =>  f.Correo).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty().EmailAddress().WithErrorCode("").WithMessage("Correo No Válido: '{0}'",f=>f.Correo);
        }
    }

    public class ValidadorActualizarClave : ValidadorBase<ActualizarClave>, IValidador<ActualizarClave>
    {
        public int MinLongitudContrasena { get; set; }
        public int MaxLongitudContrasena { get; set; }

        public ValidadorActualizarClave()
        {
            MinLongitudContrasena = 12;
            MaxLongitudContrasena = 32;
            RuleFor(f => f.Login).NotEmpty().WithErrorCode("").WithMessage("Debe Indicar el Login");
            RuleFor(f => f.AntiguaContrasena).NotEmpty().WithErrorCode("").WithMessage("Debe Indicar la actual contraseña");
            RuleFor(f => f.NuevaContrasena).NotEmpty().WithErrorCode("").WithMessage("Debe Indicar la nueva contraseña");
            RuleFor(f => f.NuevaContrasena).Length(MinLongitudContrasena, MaxLongitudContrasena).WithErrorCode("").WithMessage("{0} <= Longitud Contraseña <= {1} ", MinLongitudContrasena, MaxLongitudContrasena);
        }
    }

    public class ValidadorGenerarContrasena : ValidadorBase<GenerarContrasena>, IValidador<GenerarContrasena>
    {
        
        public ValidadorGenerarContrasena()
        {
            RuleFor(f => f.Login).NotEmpty().WithErrorCode("").WithMessage("Debe Indicar el Login");
        }
    }

}
