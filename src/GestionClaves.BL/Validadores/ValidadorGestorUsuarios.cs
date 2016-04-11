using ServiceStack.FluentValidation;
using GestionClaves.Modelos.Interfaces;
using GestionClaves.Modelos.Entidades;
using GestionClaves.Modelos.Servicio;
using System;

namespace GestionClaves.BL.Validadores
{
    public class ValidadorGestorUsuarios:IValidadorGestorUsuarios
    {

        public IValidador<ActualizarContrasena> ValidadorActualizarContrasena { get; set; }
        public IValidador<GenerarContrasena> ValidadorGenerarContrasena { get; set; }
        public IValidador<SolicitarGeneracionContrasena> ValidadorSolicitarCambio { get; set; }

        public IValidador<Usuario> ValidadorActivo { get; set; }
        public IValidador<Usuario> ValidadorActivoConCorreo { get; set; }
        

        public ValidadorGestorUsuarios()
        {
            ValidadorActualizarContrasena = new ValidadorActualizarContrasena();
            ValidadorGenerarContrasena = new ValidadorGenerarContrasena();
            ValidadorSolicitarCambio = new ValidadorSolicitarCambio();

            ValidadorActivo = new ValidadorUsuarioActivo();
            ValidadorActivoConCorreo = new ValidadorUsuarioActivoConCorreo();   
        }

        public void ValidarPeticion(ActualizarContrasena request)
        {
            ValidadorActualizarContrasena.ValidateAndThrow(request);
        }

        public void ValidarPeticion(GenerarContrasena request)
        {
            ValidadorGenerarContrasena.ValidateAndThrow(request);
        }

        public void ValidarPeticion(SolicitarGeneracionContrasena request)
        {
            ValidadorSolicitarCambio.ValidateAndThrow(request);
        }


        public void ValidarActivo(Usuario usuario)
        {
            ValidadorActivo.ValidateAndThrow(usuario);
        }
       
        public void ValidarActivoConCorreo(Usuario usuario)
        {
            ValidadorActivoConCorreo.ValidateAndThrow(usuario);
        }

        
    }
        

    public class ValidadorUsuarioActivo:ValidadorBase<Usuario>,IValidador<Usuario>
    {
        public ValidadorUsuarioActivo()
        {
            MensajeCuandoInstanciaEsNull = "Usuario / Contraseña inválidos";
            RuleFor(f => f.Activo).Cascade(CascadeMode.StopOnFirstFailure).NotNull().Must(v => v.Value==true).WithErrorCode("").WithMessage("Usuario Inactivo");  
        }                   
    }

    public class ValidadorUsuarioActivoConCorreo : ValidadorBase<Usuario>, IValidador<Usuario>
    {
        public ValidadorUsuarioActivoConCorreo()
        {
            MensajeCuandoInstanciaEsNull = "Usuario No Existe";
            RuleFor(f => f.Activo).Cascade(CascadeMode.StopOnFirstFailure).NotNull().Must(v => v.Value == true).WithErrorCode("").WithMessage("Usuario Inactivo");
            RuleFor(f => f.Email).Cascade(CascadeMode.StopOnFirstFailure).NotNull().NotEmpty().EmailAddress().WithErrorCode("").WithMessage("Correo No Válido: '{0}'", f => f.Email);
        }
    }



    public class ValidadorActualizarContrasena : ValidadorBase<ActualizarContrasena>, IValidador<ActualizarContrasena>
    {
        public int MinLongitudContrasena { get; set; }
        public int MaxLongitudContrasena { get; set; }

        public ValidadorActualizarContrasena()
        {
            MinLongitudContrasena = 12;
            MaxLongitudContrasena = 32;
            RuleFor(f => f.Usuario).NotEmpty().WithErrorCode("").WithMessage("Debe Indicar el Usuario");
            RuleFor(f => f.ContrasenaActual).NotEmpty().WithErrorCode("").WithMessage("Debe Indicar la actual contraseña");
            RuleFor(f => f.NuevaContrasena).NotEmpty().WithErrorCode("").WithMessage("Debe Indicar la nueva contraseña");
            RuleFor(f => f.NuevaContrasena).Length(MinLongitudContrasena, MaxLongitudContrasena).WithErrorCode("").WithMessage("{0} <= Longitud Contraseña <= {1} ", MinLongitudContrasena, MaxLongitudContrasena);
        }
    }

    public class ValidadorGenerarContrasena : ValidadorBase<GenerarContrasena>, IValidador<GenerarContrasena>
    {
        public ValidadorGenerarContrasena()
        {
            RuleFor(f => f.Usuario).NotEmpty().WithErrorCode("").WithMessage("Debe Indicar el Usuario");
            RuleFor(f => f.Token).NotEmpty().WithErrorCode("").WithMessage("Debe Indicar el Token");
        }
    }

    internal class ValidadorSolicitarCambio : ValidadorBase<SolicitarGeneracionContrasena>, IValidador<SolicitarGeneracionContrasena>
    {
        public ValidadorSolicitarCambio()
        {
            RuleFor(f => f.Usuario).NotEmpty().WithErrorCode("").WithMessage("Debe Indicar el Usuario");
            RuleFor(f => f.Captcha).NotEmpty().WithErrorCode("").WithMessage("Debe Indicar el texto Captcha");
        }
    }


}
