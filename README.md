# Screenshot Utility for NI InstrumentStudio

This utility captures a screenshot of the NI InstrumentStudio application using Windows APIs. It is intended for use in automated test workflows (e.g., TestStand, LabVIEW, Python) where visual evidence of instrument configuration or measurements is required.

---

## Prerequisites

Before invoking the screenshot utility, ensure the following:

- NI InstrumentStudio is already launched
- The required Instrument Soft Front Panel (SFP) is open and fully loaded
- InstrumentStudio is in a stable state (not during startup, shutdown, or dynamic reconfiguration)
- For multi-monitor setups, consistent display scaling is recommended for reliable results

> ⚠️ **Note**  
> The utility does not launch InstrumentStudio or open SFPs. It captures the current state of an already running InstrumentStudio session.

---

## Important Notes & Limitations

- The screenshot is captured from the currently open InstrumentStudio window
- The utility relies on Windows window-capture APIs and inherits their platform limitations
- In multi-monitor environments with different resolutions or DPI scaling, partial captures may occur
- Full-window screenshots are most reliable when InstrumentStudio is visible and not minimized

---

## Typical Usage Flow

1. Launch InstrumentStudio
2. Open the required instrument Soft Front Panel(s)
3. Configure the instrument as needed
4. Invoke the screenshot utility from TestStand, LabVIEW, or Python
5. The screenshot is saved to the specified output path
