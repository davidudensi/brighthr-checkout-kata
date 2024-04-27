## Bright HR Checkout Kata

The BrightHR Checkout Kata is a simple .NET Class Library designed to the checkout process of an arbitrary supermarket

## Problem

In a normal supermarket, things are identified using Stock Keeping Units, or SKUs. In our shop, we’ll use individual letters of the alphabet (A, B, C, and so on). Our goods are priced individually. In addition, some items are multipriced: buy n of them, and they’ll cost you y pounds. For example, item ‘A’ might cost 50 pounds individually, but this week we have a special offer: buy three ‘A’s and they’ll cost you 130. The current pricing and offers are as follows:

SKU Unit Price Special Price
A 50 3 for 130
B 30 2 for 45
C 20
D 15
Our checkout accepts items in any order, so that if we scan a B, an A, and another B, we’ll recognize the two B’s and price them at 45 (for a total price so far of 95). Because the pricing changes frequently, we need to be able to pass in a set of pricing rules each time we start handling a checkout transaction.

Here's a suggested interface for the checkout...
interface ICheckout
{
void Scan(string item);
int GetTotalPrice();
}

## Instructions

Implement a class library that satisfies the problem described above. The solution should be test driven.

We're as interested in the process that you go through to develop the code as the end result, so commit early and often so we can see the steps that you go through to arrive at your solution. We want to see a git repository containing your solution, ideally uploaded to your own github account.

If you've not done a kata before, there are some great reources on the web describing the process.

## Getting Started

To view and use, download the Class Library from https://github.com/davidudensi/brighthr-checkout-kata

The Class Library is developed with .NET

## Using the CheckoutKata

var rules = File.ReadAllText(rules.json);
CheckoutService checkout = new CheckoutService(rules);
checkout.Scan("ABCD");
var total = checkout.GetTotalPrice();

## Documentation
namespace CheckoutKata.Core
- /Interfaces
- /Services
- /Models
- /Dtos

The ICheckout.cs interface defines the required function of the checkout kata

public interface ICheckout
{
    public void Scan(string item);
    public int GetTotalPrice();
}

The Services.CheckoutService.cs class implements the ICheckout interface

public CheckoutService(string rules, ILogger<CheckoutService> logger)
- rules: string json, defining the pricing rules
- logger: ILogger for logging

public void Scan(string items)
- items: a string literals representing the items to be scanned (e.g. "ABCD")

public int GetTotalPrice()
- returns the calculated total for all items scanned

## Unit Tests

Unit Test Packages: 
- xUnit
- FluentAssersions
- Moq

Namespace: CheckoutKata.Tests
- /Systems
    - /Models
    - /Services
- /Fixtures

## Getting started with testing

To run the uni test, I have created different pricing rules (CheckoutKata.Tests.Fixtures.Rules) in json format that will be used when instantiating the CheckoutService class.
Ensure these files are in the debug directory (CheckoutKata.Tests/bin/Debug/net6.0)

# Rule format (rules.json)
[
  {
    "SKU": "A",
    "UnitPrice": 50,
    "SpecialPrice": {
      "Units": 3,
      "Price": 130
    }
  },
  {
    "SKU": "B",
    "UnitPrice": 30,
    "SpecialPrice": {
      "Units": 2,
      "Price": 45
    }
  },
  {
    "SKU": "C",
    "UnitPrice": 20,
    "SpecialPrice": null
  },
  {
    "SKU": "D",
    "UnitPrice": 15,
    "SpecialPrice": null
  }
]

# Wrong rule case (rules-wrong-format.json)

{
  "SKU": "A",
  "UnitPrice": 50,
  "SpecialPrice": {
    "Units": 3,
    "Price": 130
  }
}

# Missing SKU rule (rules-two.json)

[
  {
    "SKU": "A",
    "UnitPrice": 50,
    "SpecialPrice": {
      "Units": 3,
      "Price": 130
    }
  },
  {
    "SKU": "",
    "UnitPrice": 30,
    "SpecialPrice": null
  }
]

# Two rules with same SKU (rules-replace.json)

[
  {
    "SKU": "A",
    "UnitPrice": 50,
    "SpecialPrice": {
      "Units": 3,
      "Price": 130
    }
  },
  {
    "SKU": "A",
    "UnitPrice": 75,
    "SpecialPrice": {
      "Units": 3,
      "Price": 200
    }
  }
]

