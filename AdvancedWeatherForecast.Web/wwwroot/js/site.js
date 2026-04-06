document.addEventListener("DOMContentLoaded", () => {
    const yearNode = document.querySelector("[data-current-year]");
    if (yearNode) {
        yearNode.textContent = new Date().getFullYear().toString();
    }

    const timeNodes = document.querySelectorAll("[data-current-time]");
    const formatTime = () => {
        const now = new Date();
        const label = now.toLocaleString([], {
            weekday: "short",
            month: "short",
            day: "numeric",
            hour: "numeric",
            minute: "2-digit"
        });

        timeNodes.forEach((node) => {
            node.textContent = label;
        });
    };

    if (timeNodes.length > 0) {
        formatTime();
        window.setInterval(formatTime, 60000);
    }

    document.querySelectorAll("[data-scroll-target]").forEach((button) => {
        button.addEventListener("click", () => {
            const selector = button.getAttribute("data-scroll-target");
            const target = selector ? document.querySelector(selector) : null;
            if (target) {
                target.scrollIntoView({ behavior: "smooth", block: "start" });
            }
        });
    });

    document.querySelectorAll("[data-dismiss-alert]").forEach((button) => {
        button.addEventListener("click", () => {
            const card = button.closest("[data-dismissible]");
            if (card) {
                card.classList.add("is-dismissing");
                window.setTimeout(() => card.remove(), 180);
            }
        });
    });
});
