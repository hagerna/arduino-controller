import express from "express";
import http from "http";
import { SerialPort } from "serialport";
import { ReadlineParser } from "@serialport/parser-readline";

/**
 * Setup express server and websocket on port 3000
 */
const app = express();
app.use(express.json());
const server = http.createServer(app);
const serverPort = 3000;

/**
 * Setup serial port listener for Arduino
 */
const port = new SerialPort({
  path: "/dev/cu.usbmodem1101", // Check Arduino IDE > Tools for this
  baudRate: 9600,
});

port.on("open", () => {
  console.log("Serial port opened");
});

const parser = port.pipe(new ReadlineParser({ delimiter: "\n" }));
parser.on("data", (data) => {
  console.log(data);
  let input = JSON.parse(data);
  server.emit("data", input);
});

app.get("/", (req, res) => {
  res.send("Hello from Express!");
});

server.listen(serverPort, () => {
  console.log("Arduino server is running on port 3000");
});
