import express from "express";
import http from "http";
import { WebSocket, WebSocketServer } from "ws";
import { SerialPort } from "serialport";
import { ReadlineParser } from "@serialport/parser-readline";
import { randomUUID } from "crypto";

/**
 * Setup express server and websocket on port 3005
 */
var connections = new Map([]);

const app = express();
app.use(express.json());
const server = http.createServer(app);

// Create a WebSocket server
const wss = new WebSocketServer({ server });
/**
 * Setup serial port listener for Arduino
 */
function setupSerialPort() {
  try {
    const port = new SerialPort({
      path: "/dev/cu.usbmodem1101", // Check Arduino IDE > Tools for this
      baudRate: 9600,
    });

    port.on("open", () => {
      console.log("Serial port opened");
    });

    const parser = port.pipe(new ReadlineParser({ delimiter: "\n" }));
    parser.on("data", (data) => {
      try {
        if (connections.size > 0) {
          console.log(data);
          connections.forEach((socket) => {
            socket.send(data);
          });
        }
      } catch (error) {
        console.log(error);
      }
    });
  } catch (error) {
    console.log(
      "Failed to connect to Serial port. Please make sure Arduino is connected and running."
    );
  }
}

setupSerialPort();

wss.on("connection", (socket) => {
  console.log("WebSocket connected!");
  const id = randomUUID();
  connections.set(id, socket);
  socket.on("close", () => {
    console.log("WebSocket disconnected");
    connections.delete(id);
  });
});

/**
 * Setup express server and websocket on port 3005
 */
const serverPort = 3005;
server.listen(serverPort, () => {
  console.log("Arduino server is running on port 3000");
});
