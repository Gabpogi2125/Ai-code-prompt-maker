// Free tier: 5 prompts/day
let freeRequests = Number(localStorage.getItem("freeRequests")) || 5;
let paidActive = localStorage.getItem("paidActive") === "true";

document.getElementById("credits").innerText = paidActive ? "∞" : freeRequests;

// Main prompt generator
function generatePrompt() {
  if (!paidActive && freeRequests <= 0) {
    alert("Free requests used up! Upgrade to paid plan.");
    return;
  }

  const desc = document.getElementById("botDesc").value.trim();
  if (!desc) {
    alert("Please describe the bot you want.");
    return;
  }

  // Generate a long professional AI prompt
  const prompt = `Generate a complete Python bot based on the following description:
"${desc}"
Requirements:
- Full code with main.py and requirements.txt
- Include clear comments explaining each section
- Use best practices for Python coding
- Ensure the bot is safe and non-malicious
- Add extra useful features the AI thinks are appropriate
- Do NOT generate harmful, destructive, or illegal code
- Output should be ready for ChatGPT or OpenRouter to process`;

  document.getElementById("aiPrompt").value = prompt;

  if (!paidActive) {
    freeRequests--;
    localStorage.setItem("freeRequests", freeRequests);
    document.getElementById("credits").innerText = freeRequests;
  }
}

// Copy prompt to clipboard
function copyPrompt() {
  const prompt = document.getElementById("aiPrompt").value;
  if (!prompt) return;
  navigator.clipboard.writeText(prompt).then(() => {
    alert("Prompt copied to clipboard!");
  });
}

// Download simple templates
function downloadTemplates() {
  const mainPy = `# main.py template\nprint("Hello, this is your bot!")`;
  const req = `# requirements.txt template\ndiscord.py\n`;

  downloadFile("main.py", mainPy);
  downloadFile("requirements.txt", req);
}

function downloadFile(filename, content) {
  const blob = new Blob([content], { type: "text/plain" });
  const a = document.createElement("a");
  a.href = URL.createObjectURL(blob);
  a.download = filename;
  a.click();
}

// Paid key activation (manual key via Discord)
const YOUR_PAID_KEY = "AICODER-PAID-2026"; // Change per user

function activatePaidKey() {
  const inputKey = document.getElementById("paidKey").value.trim();
  if (inputKey === YOUR_PAID_KEY) {
    paidActive = true;
    localStorage.setItem("paidActive", "true");
    document.getElementById("credits").innerText = "∞";
    alert("Paid plan activated! Unlimited prompts unlocked.");
  } else {
    alert("Invalid key. Please contact us via Discord.");
  }
}
