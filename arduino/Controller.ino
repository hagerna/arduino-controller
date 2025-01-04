int leftPrimaryState = 0;
int rightPrimaryState = 0;
int leftSecondaryState = 0;
int rightSecondaryState = 0;

void setup() {
  // put your setup code here, to run once:
  pinMode(2, INPUT); // Left Primary
  pinMode(4, INPUT); // Right Primary
  pinMode(3, INPUT); // Left Secondary
  pinMode(7, INPUT); // Right Secondary
  Serial.begin(9600);

}

void writeButtonState(char *name, int state, bool withSeparator) {
  Serial.write("\"");
  Serial.write(name);
  Serial.write("\":");
  if (state == LOW){
    Serial.write("1");
  }
  else {
    Serial.write("0");
  }
  if (withSeparator){
    Serial.write(",");
  }
}

void loop() {
  // put your main code here, to run repeatedly:
  leftPrimaryState = digitalRead(2);
  rightPrimaryState = digitalRead(4);
  leftSecondaryState = digitalRead(3);
  rightSecondaryState = digitalRead(7);
  Serial.write("{\"arduino_inputs\": {");
  writeButtonState("leftPrimaryButton", leftPrimaryState, true);
  writeButtonState("rightPrimaryButton", rightPrimaryState, true);
  writeButtonState("leftSecondaryButton", leftSecondaryState, true);
  writeButtonState("rightSecondaryButton", rightSecondaryState, false);
  Serial.write("}}\n");
  delay(1);
}
