using AppCleanArchitecture.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AppCleanArchitecture.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, 99, "product image");
            action.Should()
                .NotThrow<AppCleanArchitecture.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product name", "Product Description", 9.99m, 99, "product image");
            action.Should()
                .Throw<AppCleanArchitecture.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id value");
        }

        [Fact]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "product image");
            action.Should()
                .Throw<AppCleanArchitecture.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characteres");
        }

        [Fact]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m,
                99, "proooooooooooooooooooooooooooooooooooodddddddddddddddddddddddddddddddddddddddddddduct proooooooooooooooooooooooooooooooooooodddddddddddddddddddddddddddddddddddddddddddduct proooooooooooooooooooooooooooooooooooodddddddddddddddddddddddddddddddddddddddddddduct proooooooooooooooooooooooooooooooooooodddddddddddddddddddddddddddddddddddddddddddduct proooooooooooooooooooooooooooooooooooodddddddddddddddddddddddddddddddddddddddddddduct iiiiiiiiiiiiiiiiiiiiiimmmmmmmmmmmmmmmmmmmmmmaaaaaaaaaaaaaaaaaggggggggggggggggggeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
            action.Should()
                .Throw<AppCleanArchitecture.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid image name, too long, maximum 250 characteres");
        }

        [Fact]
        public void CreateProduct_WithNullImageValue_NoDomainException()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, 99, null);
            action.Should()
                .NotThrow<AppCleanArchitecture.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_WithNullImageValue_NoNullReferenceException()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, 99, null);
            action.Should()
                .NotThrow<NullReferenceException>();
        }

        [Fact]
        public void CreateProduct_WithEmptyImageValue_NoDomainException()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, 99, "");
            action.Should()
                .NotThrow<AppCleanArchitecture.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_InvalidPriceValue_DomainException()
        {
            Action action = () => new Product(1, "Product name", "Product Description", -9.99m, 99, "product image");
            action.Should()
                .Throw<AppCleanArchitecture.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid price value");
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product(1, "Pro", "Product Description", 9.99m, value, "product image");
            action.Should()
                .Throw<AppCleanArchitecture.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid stock value");
        }
    }
}
