
using BackOfficeApi.Controllers;
using BackOfficeApi.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace BackOfficeApi.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly UsersController _controller;
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private readonly Mock<SignInManager<IdentityUser>> _signInManagerMock;

        public UserControllerTests()
        {
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);

            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStoreMock.Object, null!, null!, null!, null!);

            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            _signInManagerMock = new Mock<SignInManager<IdentityUser>>(_userManagerMock.Object, contextAccessorMock.Object, claimsFactoryMock.Object, null!, null!, null!, null!);

            _controller = new UsersController(_userManagerMock.Object, _roleManagerMock.Object, _signInManagerMock.Object);
        }

        // Método auxiliar para extrair a mensagem do resultado
        private static string? GetMessageFromResult(IActionResult result)
        {
            var okResult = result as OkObjectResult;
            if (okResult?.Value is Dictionary<string, string> dictResponse && dictResponse.ContainsKey("Message"))
            {
                return dictResponse["Message"];
            }
            return okResult?.Value?.GetType().GetProperty("Message")?.GetValue(okResult.Value, null) as string;
        }

        [Fact]
        public async Task Register_ReturnsOk_WhenUserIsRegisteredSuccessfully()
        {
            var model = new RegisterUserDTO { Email = "test@example.com", Role = "User" };
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _roleManagerMock.Setup(rm => rm.RoleExistsAsync(model.Role)).ReturnsAsync(true);

            var result = await _controller.Register(model);

            var messageValue = GetMessageFromResult(result);
            Assert.NotNull(messageValue);
            Assert.Equal("Usuário registrado com sucesso!", messageValue);
        }

        [Fact]
        public async Task Register_AddsUserToRole_WhenRoleExists()
        {
            var model = new RegisterUserDTO { Email = "test2@example.com", Role = "Admin" };
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _roleManagerMock.Setup(rm => rm.RoleExistsAsync(model.Role)).ReturnsAsync(true);
            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<IdentityUser>(), model.Role))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _controller.Register(model);

            var messageValue = GetMessageFromResult(result);
            Assert.NotNull(messageValue);
            Assert.Equal("Usuário registrado com sucesso!", messageValue);
            _userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<IdentityUser>(), model.Role), Times.Once);
        }

        [Fact]
        public async Task Login_ReturnsOk_WhenLoginIsSuccessful()
        {
            var model = new LoginDTO { Email = "test@example.com", Password = "SenhaFort3!" };
            _signInManagerMock.Setup(sm => sm.PasswordSignInAsync(model.Email, model.Password, false, true))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var result = await _controller.Login(model);

            var messageValue = GetMessageFromResult(result);
            Assert.NotNull(messageValue);
            Assert.Equal("Login bem-sucedido", messageValue);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenLoginFails()
        {
            var model = new LoginDTO { Email = "test@example.com", Password = "WrongPassword" };
            _signInManagerMock.Setup(sm => sm.PasswordSignInAsync(model.Email, model.Password, false, true))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var result = await _controller.Login(model);

            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Credenciais inválidas.", unauthorizedResult.Value);
        }
    }
}