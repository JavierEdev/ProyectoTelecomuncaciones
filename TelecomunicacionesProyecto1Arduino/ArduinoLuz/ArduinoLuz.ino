const float GAMMA = 0.7;
const float RL10 = 50;

void setup() {
  Serial.begin(9600);
}

void loop() {
  int analogValue = analogRead(A0);
  float voltage = analogValue / 1024.0 * 5.0;
  float resistance = 2000 * voltage / (1 - voltage / 5.0);
  float lux = pow(RL10 * 1e3 * pow(10, GAMMA) / resistance, (1.0 / GAMMA));

  Serial.println(lux);
  delay(500);
}
