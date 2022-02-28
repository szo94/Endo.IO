# endo.io
A smart insulin basal rate optimization assistant

## What is basal insulin therapy?
The following is a very brief explanation of basal insulin therapy, adapted from Wikipedia:

In type 1 diabetes, insulin production is extremely low, and as such the body requires exogenous insulin. Basal insulin regulates the body's blood glucose between mealtimes, as well as overnight. One way to achieve this is via continuous infusion of rapid-acting insulin using an insulin pump.

## Project goal
Historically, changes to basal rates are infrequent occurences, suggested by endocrinologists during annual or bi-annual checkup visits. Optimizating this way can be a very slow process, and is not responsive to changes in metabolism, activity levels, and many other internal and environmental factors.

This project aims to produce a smart assistant which one can feed recent blood glucose data to receive suggestions for possible basal rate optimizations. This assistant will process two weeks of data at a time, allowing for quicker, iterative optimization.