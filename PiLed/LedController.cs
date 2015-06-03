using System;
using Windows.Devices.Gpio;

namespace PiLed
{
    public class LedController : IDisposable
    {
        private const int SRCLK_PIN = 13; // PIN 11 -> GPIO 13
        private GpioPin shiftRegisterClock;

        private const int SER_PIN = 0; // PIN 14 -> GPIO 0
        private GpioPin data;

        private const int RCLK_PIN = 6; // PIN 12 -> GPIO 6
        private GpioPin registerClock;

        private const int OE_PIN = 5; // PIN 13 -> GPIO 5
        private GpioPin outputEnable;

        private const int SRCLR_PIN = 26; // PIN 10 -> GPIO 26
        private GpioPin shiftRegisterClear;

        public string InitializeSystem()
        {
            // initialize the GPIO pins we will use for bit-banging our data data to the shift register
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                return "There is no GPIO controller on this device.";
            }

            // setup the RPi2 GPIO that controls the shift register
            shiftRegisterClock = gpio.OpenPin(SRCLK_PIN);
            data = gpio.OpenPin(SER_PIN);
            registerClock = gpio.OpenPin(RCLK_PIN);
            outputEnable = gpio.OpenPin(OE_PIN);
            shiftRegisterClear = gpio.OpenPin(SRCLR_PIN);

            // Show an error if the pin wasn't initialized properly
            if (shiftRegisterClock == null || data == null || registerClock == null || outputEnable == null || shiftRegisterClear == null)
            {
                return "There were problems initializing the GPIO pin.";
            }

            // Set all pins in outpu mode
            shiftRegisterClock.SetDriveMode(GpioPinDriveMode.Output);
            data.SetDriveMode(GpioPinDriveMode.Output);
            registerClock.SetDriveMode(GpioPinDriveMode.Output);
            outputEnable.SetDriveMode(GpioPinDriveMode.Output);
            shiftRegisterClear.SetDriveMode(GpioPinDriveMode.Output);

            ClearOutput();
            EnableOutput();

            return "GPIO pin initialized correctly.";
        }

        public void ClearOutput()
        {
            shiftRegisterClear.Write(GpioPinValue.Low);
            shiftRegisterClear.Write(GpioPinValue.High);
        }

        public void EnableOutput()
        {
            outputEnable.Write(GpioPinValue.Low);
        }

        public void DisableOuput()
        {
            outputEnable.Write(GpioPinValue.High);
        }

        public void Write(byte d)
        {
            for (uint i = 1; i < 256; i = i << 1)
            {
                data.Write((d & i) == i ? GpioPinValue.High : GpioPinValue.Low);
                shiftRegisterClock.Write(GpioPinValue.High);
                shiftRegisterClock.Write(GpioPinValue.Low);
            }

            registerClock.Write(GpioPinValue.High);
            registerClock.Write(GpioPinValue.Low);
        }

        public void Dispose()
        {
            shiftRegisterClock.Dispose();
            data.Dispose();
            registerClock.Dispose();
            outputEnable.Dispose();
            shiftRegisterClear.Dispose();
        }
    }
}
