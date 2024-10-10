# Enumerables

## Transfer Types
- For Storage
- For Irradiation
- For Release
- Non-Conformity Item 

## Sales Order & Batch Status

- Received - For Storage
- In Storage – For Irradiation
- For Irradiation
- At Irradiation
- Irradiated - For QA
- Irradiated - In Storage - For QA
- For Dispatch

### Received - For Storage

> item is received already but not yet in bin for items whose Service Type is "Storage-Irradiation-Storage (SIS)" and "Storage Only"

### In Storage – For Irradiation

> For "SIS" items in bin already

### For Irradiation

> Received items whose Service Type is "Irradiation Only"

### At Irradiation
> Items already in Irradiation Loading Bay

### Irradiated - For QA
> Items are already at Irradiation Unloading Bay, not yet in bin

### Irradiated - In Storage - For QA
> Items that are transferred from Irrad Unloading Bay to bin Service Type is SIS

### For Dispatch
> Items whose Service Type is "Irradiation Only" transfer from Irradiation Unloading Bay to dispatch/release warehouse
