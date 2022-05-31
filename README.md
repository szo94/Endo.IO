# Endo.IO
A smart insulin basal rate optimization assistant

## What is basal insulin therapy?
The following is a very brief explanation of basal insulin therapy, adapted from Wikipedia:

In type 1 diabetes, insulin production is extremely low, and as such the body requires exogenous insulin. Basal insulin therapy is most commonly used to regulate the body's blood glucose between mealtimes and overnight. One way to achieve this is via continuous infusion of rapid-acting insulin using an insulin pump.

## Project goal
Historically, changes to basal rates are infrequent occurences suggested by endocrinologists during annual or bi-annual checkup visits. Relying solely on these occurences can result in a very slow optimization process which is only minimally responsive to changes in metabolism, activity levels, and other internal and environmental factors.

This project aims to build a smart assistant which will examine recent blood glucose data and suggest changes to current basal rates and/or bolusing ratios.

## Project Status
At present, the program reads in a Dexcom Clarity export in the form of a cleaned CSV and prints results to the console. I am working on a REST client to pull data directly from Dexcom and other CGM data providers to allow for automatic, bi-weekly email prompts.

In addition, I am working with a clinical endocrinologist to define common clinical approaches and develop strategies for treatment customization.
