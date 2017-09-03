using System;
using Shouldly;
using Xunit;

namespace MaybeMonad.Tests
{
    [Trait("Monad", "Maybe")]
    public class MaybeTests
    {
        [Fact]
        public void Maybe_nothing_is_empty()
        {
            var emptyMaybe = Maybe<string>.Nothing;
            emptyMaybe.HasNoValue.ShouldBeTrue();
        }

        [Fact]
        public void Maybe_from_null_is_empty()
        {
            var emptyMaybe = Maybe.From<string>(null);
            emptyMaybe.HasValue.ShouldBeFalse();
            emptyMaybe.HasNoValue.ShouldBeTrue();
        }

        [Fact]
        public void Maybe_from_value_is_not_empty()
        {
            var value = "abc";
            var maybe = Maybe.From(value);
            maybe.HasValue.ShouldBeTrue();
            maybe.HasNoValue.ShouldBeFalse();
        }

        [Fact]
        public void Maybe_from_value_contains_value()
        {
            var value = "abc";
            var maybe = Maybe.From(value);
            maybe.Value.ShouldBe(value);
        }

        [Fact]
        public void Acessing_the_value_of_empty_maybe_throws_exception()
        {
            var emptyMaybe = Maybe<string>.Nothing;
            var exception = Should.Throw<InvalidOperationException>(() =>
            {
                var value = emptyMaybe.Value;
            });
        }

        [Fact]
        public void Implicit_operator_creates_maybe_with_value()
        {
            var value = "abc";
            Maybe<string> maybe = value;
            maybe.Value.ShouldBe(value);
        }

        [Fact]
        public void Implicit_operator_creates_empty_maybe_if_value_is_null()
        {
            string value = null;
            Maybe<string> maybe = value;
            maybe.ShouldBe(Maybe<string>.Nothing);
        }
    }
}
