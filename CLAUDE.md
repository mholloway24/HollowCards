# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

HollowCards is an extensible C# class library for creating and managing playing card decks. It is designed to support any card-based game — from classic games (Solitaire, Blackjack) to customizable games like Magic: The Gathering — through a configuration-driven architecture.

## Commands

All commands run from `src/HollowCards/`.

```bash
# Build
dotnet build HollowCards.sln

# Run all tests
dotnet test HollowCards.UnitTests

# Run a single test class
dotnet test HollowCards.UnitTests --filter "FullyQualifiedName~CardConfigurationTests"

# Run a single test method
dotnet test HollowCards.UnitTests --filter "FullyQualifiedName~EnsureDeckCardCount"

# Run the console demo (stress-tests 10,000 decks)
dotnet run --project HollowCards.Console
```

## Architecture

### Core Abstraction: `ICardsConfiguration`

Everything in the library flows from `ICardsConfiguration` (`HollowCards/Interface/ICardsConfiguration.cs`). A configuration defines:
- How many cards are in a deck (`NumberOfCardsInDeck`)
- The ordered list of `Card` objects for a deck (`ConfigureDeck()`)
- Face-value-to-numeric-value mapping (`FaceValueMapping`, `GetCardValue()`)
- How a card is displayed (`GetDisplayValue()`)

To support a new game, implement `ICardsConfiguration` and register it with `CardConfigurationFactory.RegisterConfiguration()`.

### Built-in Configurations

Registered by name in `CardConfigurationFactory` (static constructor):

| Name constant (`CardConfiguration.*`) | Cards | Notes |
|---|---|---|
| `TraditionalNoJokers` | 52 | Ace = 1 |
| `TraditionalAceHigh` | 52 | Ace = 11 |
| `TraditionalJokers` | 54 | Adds Little Joker + Big Joker |

### Factory and Instantiation

`CardConfigurationFactory` (`HollowCards/Utility/CardConfigurationFactory.cs`) is a static registry that maps string names to `Type` objects. It uses `Activator.CreateInstance` to instantiate configurations, so **custom configurations must have a public parameterless constructor**.

`Deck` can be constructed with either an `ICardsConfiguration` instance or a registered name string — the string overload calls through the factory.

### Deck vs. SuperDeck

- **`Deck`** — single deck. Uses `RNGCryptoServiceProvider` with rejection sampling (`IsFairChoice`) for cryptographically unbiased shuffling. `Deal()` auto-reshuffles when exhausted.
- **`SuperDeck`** — multiple decks combined. Shuffles each constituent deck in parallel (`Parallel.ForEach`, max 4 threads), then concatenates.

### Card Extended Properties

`Card` objects carry an `ExtendedProperties` dictionary (`Constants.SuitProperty`, `Constants.FaceProperty`) for metadata. Jokers intentionally omit the suit property — tests in `CardConfigurationTests.TestCardSuits` verify this boundary.

### Projects in the Solution

- `HollowCards` — the class library (main deliverable)
- `HollowCards.UnitTests` — xUnit test suite (use this for all testing)
- `HollowCards.Tests` — placeholder project, empty
- `HollowCards.Console` — demo/stress-test executable, not a test suite
