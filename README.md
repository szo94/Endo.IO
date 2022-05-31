# Endo.IO
A smart insulin basal rate optimization assistant

## What is basal insulin therapy?
The following is a very brief explanation of basal insulin therapy, adapted from Wikipedia:

In type 1 diabetes, insulin production is extremely low, and as such the body requires exogenous insulin. Basal insulin therapy is most commonly used to regulate the body's blood glucose between mealtimes and overnight. One way to achieve this is via continuous infusion of rapid-acting insulin using an insulin pump.

## Project goal
Historically, changes to basal rates are infrequent occurences suggested by endocrinologists during annual or bi-annual checkup visits. Relying solely on these occurences can result in a very slow optimization process which is only minimally responsive to changes in metabolism, activity levels, and other internal and environmental factors.

This project aims to build a smart assistant which will examine recent blood glucose data and suggest changes to current basal rates and/or bolusing ratios.

## Project Status
In its current, preliminary form, the program reads in a CSV formatted as a Dexcom Clarity export and prints results to the console. I aim to eventually translate the project into Node and create a React front-end, though I am still working to familiarize myself with both of those libraries. I also intend to introduce a call to the Dexcom API to allow for automatic, bi-weekly email prompts.

In addition, I am working with a pair of clinical endocrinologists to define common clinical approaches to hone and expand the tool's capabilities, as well as to devise an effective model of interaction.
