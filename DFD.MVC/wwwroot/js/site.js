// ============================================
// FileGuard - Duplicate File Detection System
// ============================================

document.addEventListener('DOMContentLoaded', function () {
      initUploadZone();
      loadStorageInfo();
      loadFolders();
});

// Upload Modal
function openUploadModal() {
      document.getElementById('uploadModal').classList.add('active');
      resetUploadForm();
}

function closeUploadModal() {
      document.getElementById('uploadModal').classList.remove('active');
}

function resetUploadForm() {
      document.getElementById('uploadForm').reset();
      document.getElementById('filePreview').style.display = 'none';
      document.getElementById('duplicateAlert').style.display = 'none';
      document.getElementById('uploadBtn').disabled = true;
      document.getElementById('previewHash').textContent = 'جاري الحساب...';
}

// Upload Zone with Drag & Drop
function initUploadZone() {
      const zone = document.getElementById('uploadZone');
      const input = document.getElementById('fileInput');

      zone.addEventListener('click', () => input.click());

      zone.addEventListener('dragover', (e) => {
            e.preventDefault();
            zone.classList.add('dragover');
      });

      zone.addEventListener('dragleave', () => {
            zone.classList.remove('dragover');
      });

      zone.addEventListener('drop', (e) => {
            e.preventDefault();
            zone.classList.remove('dragover');
            const files = e.dataTransfer.files;
            if (files.length > 0) {
                  input.files = files;
                  handleFileSelect(files[0]);
            }
      });

      input.addEventListener('change', (e) => {
            if (e.target.files.length > 0) {
                  handleFileSelect(e.target.files[0]);
            }
      });
}

// File Selection Handler
async function handleFileSelect(file) {
      const preview = document.getElementById('filePreview');
      const nameEl = document.getElementById('previewName');
      const sizeEl = document.getElementById('previewSize');
      const hashEl = document.getElementById('previewHash');
      const iconEl = document.getElementById('previewIcon');
      const uploadBtn = document.getElementById('uploadBtn');

      preview.style.display = 'block';
      nameEl.textContent = file.name;
      sizeEl.textContent = formatFileSize(file.size);
      uploadBtn.disabled = true;

      // Set icon based on type
      iconEl.className = 'fas ' + getFileIcon(file.type);

      // Calculate hash
      try {
            const hash = await calculateFileHash(file);
            hashEl.textContent = hash;

            // Check for duplicate
            const isDuplicate = await checkDuplicate(hash);

            if (isDuplicate) {
                  showDuplicateWarning(file.name);
                  uploadBtn.disabled = true;
            } else {
                  document.getElementById('duplicateAlert').style.display = 'none';
                  uploadBtn.disabled = false;
            }
      } catch (err) {
            hashEl.textContent = 'خطأ في الحساب';
            console.error(err);
      }
}

// Calculate SHA-256 Hash (Client-side)
async function calculateFileHash(file) {
      const buffer = await file.arrayBuffer();
      const hashBuffer = await crypto.subtle.digest('SHA-256', buffer);
      const hashArray = Array.from(new Uint8Array(hashBuffer));
      return hashArray.map(b => b.toString(16).padStart(2, '0')).join('');
}

// Check Duplicate via API
async function checkDuplicate(hash) {
      try {
            const response = await fetch(`/api/files/check-duplicate?hash=${hash}`);
            const data = await response.json();
            return data.isDuplicate;
      } catch (err) {
            console.error('Error checking duplicate:', err);
            return false;
      }
}

function showDuplicateWarning(fileName) {
      const alert = document.getElementById('duplicateAlert');
      const msg = document.getElementById('duplicateMsg');
      alert.style.display = 'flex';
      msg.textContent = `الملف "${fileName}" موجود بالفعل في النظام بنفس المحتوى (نفس الـ Hash).`;
}

// Load Folders for Select
async function loadFolders() {
      try {
            const response = await fetch('/api/folders');
            const folders = await response.json();
            const select = document.getElementById('folderSelect');

            folders.forEach(folder => {
                  const option = document.createElement('option');
                  option.value = folder.id;
                  option.textContent = folder.name;
                  select.appendChild(option);
            });
      } catch (err) {
            console.error('Error loading folders:', err);
      }
}

// Storage Info
async function loadStorageInfo() {
      try {
            const response = await fetch('/api/dashboard/storage');
            const data = await response.json();

            const fill = document.getElementById('storageFill');
            const text = document.getElementById('storageText');

            const percentage = (data.used / data.total) * 100;
            fill.style.width = percentage + '%';
            text.textContent = `${formatFileSize(data.used)} / ${formatFileSize(data.total)}`;
      } catch (err) {
            console.error('Error loading storage info:', err);
      }
}

// Toast Notifications
function showToast(message, type = 'success') {
      const container = document.getElementById('toastContainer');
      const toast = document.createElement('div');
      toast.className = `toast ${type}`;

      const icons = {
            success: 'fa-check-circle',
            error: 'fa-circle-xmark',
            warning: 'fa-triangle-exclamation'
      };

      toast.innerHTML = `
        <i class="fas ${icons[type] || icons.success}"></i>
        <span>${message}</span>
    `;

      container.appendChild(toast);

      setTimeout(() => {
            toast.style.opacity = '0';
            toast.style.transform = 'translateX(-20px)';
            setTimeout(() => toast.remove(), 300);
      }, 4000);
}

// Close modal on Escape key
document.addEventListener('keydown', (e) => {
      if (e.key === 'Escape') {
            closeUploadModal();
      }
});

// Close modal on overlay click
document.getElementById('uploadModal').addEventListener('click', (e) => {
      if (e.target === e.currentTarget) {
            closeUploadModal();
      }
});